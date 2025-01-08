import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, Input, message, Select } from 'antd';
import ApiClient, { Product } from '../Api';

const apiClient = new ApiClient('https://localhost:7173'); // Replace with your API URL

interface Warehouse {
  id: string;
  name: string;
  location: string;
  products: Product[];
}

const Warehouses: React.FC = () => {
  const [warehouses, setWarehouses] = useState<Warehouse[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isDetailsModalVisible, setIsDetailsModalVisible] = useState(false);
  const [editingWarehouse, setEditingWarehouse] = useState<Warehouse | null>(null);
  const [warehouseProducts, setWarehouseProducts] = useState<Product[]>([]);
  const [availableProducts, setAvailableProducts] = useState<Product[]>([]);
  const [selectedProduct, setSelectedProduct] = useState<string | null>(null);
  const [form] = Form.useForm();

  const fetchWarehouses = async () => {
    try {
      const response = await apiClient.getWarehouses();
      setWarehouses(response.data as Warehouse[]);
    } catch (error) {
      message.error('Failed to fetch warehouses');
    }
  };

  const fetchProducts = async () => {
    try {
      const response = await apiClient.getProducts();
      setAvailableProducts(response.data as Product[]);
    } catch (error) {
      message.error('Failed to fetch products');
    }
  };

  const handleCreateOrUpdate = async (values: Omit<Warehouse, 'id' | 'warehouseProducts'>) => {
    try {
      if (editingWarehouse) {
        await apiClient.updateWarehouse(editingWarehouse.id, {
          ...editingWarehouse, ...values,
          warehouseProducts: [editingWarehouse.products]
        });
        message.success('Warehouse updated successfully');
      } else {
        await apiClient.createWarehouse({ ...values, warehouseProducts: [] });
        message.success('Warehouse created successfully');
      }
      fetchWarehouses();
      setIsModalVisible(false);
      form.resetFields();
    } catch (error) {
      message.error('Failed to save warehouse');
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.deleteWarehouse(id);
      message.success('Warehouse deleted successfully');
      fetchWarehouses();
    } catch (error) {
      message.error('Failed to delete warehouse');
    }
  };

  const handleViewDetails = (warehouse: Warehouse) => {
    setEditingWarehouse(warehouse);
    console.log(warehouse);
    console.log(warehouse.products);
    setWarehouseProducts(warehouse.products);
    setIsDetailsModalVisible(true);
  };

  const handleAddProduct = async () => {
    if (editingWarehouse && selectedProduct) {
      try {
        await apiClient.addProductToWarehouse(editingWarehouse.id, selectedProduct);
        message.success('Product added to warehouse');
        fetchWarehouses();
        handleViewDetails(editingWarehouse); // Refresh details
      } catch (error) {
        message.error('Failed to add product to warehouse');
      }
    }
  };

  const handleRemoveProduct = async (productId: string) => {
    if (editingWarehouse) {
      try {
        await apiClient.removeProductFromWarehouse(editingWarehouse.id, productId);
        message.success('Product removed from warehouse');
        fetchWarehouses();
        handleViewDetails(editingWarehouse); // Refresh details
      } catch (error) {
        message.error('Failed to remove product from warehouse');
      }
    }
  };

  useEffect(() => {
    fetchWarehouses();
    fetchProducts();
  }, []);

  return (
    <div>
      <h2>Warehouses</h2>
      <Button type="primary" onClick={() => setIsModalVisible(true)}>Add Warehouse</Button>
      <Table<Warehouse>
        dataSource={warehouses}
        rowKey="id"
        columns={[
          { title: 'ID', dataIndex: 'id', key: 'id' },
          { title: 'Name', dataIndex: 'name', key: 'name' },
          { title: 'Location', dataIndex: 'location', key: 'location' },
          {
            title: 'Actions',
            render: (_, record) => (
              <>
                <Button
                  type="link"
                  onClick={() => {
                    setEditingWarehouse(record);
                    setIsModalVisible(true);
                    form.setFieldsValue(record);
                  }}
                >
                  Edit
                </Button>
                <Button type="link" onClick={() => handleViewDetails(record)}>Details</Button>
                <Button type="link" danger onClick={() => handleDelete(record.id)}>Delete</Button>
              </>
            ),
          },
        ]}
      />
      <Modal
        title={editingWarehouse ? 'Edit Warehouse' : 'Add Warehouse'}
        visible={isModalVisible}
        onCancel={() => {
          setIsModalVisible(false);
          setEditingWarehouse(null);
          form.resetFields();
        }}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleCreateOrUpdate}>
          <Form.Item
            name="name"
            label="Name"
            rules={[{ required: true, message: 'Please input the name!' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="location"
            label="Location"
            rules={[{ required: true, message: 'Please input the location!' }]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
      <Modal
        title="Warehouse Details"
        visible={isDetailsModalVisible}
        onCancel={() => {
          setIsDetailsModalVisible(false);
          setEditingWarehouse(null);
          setWarehouseProducts([]);
        }}
        footer={null}
      >
        <Table<Product>
          dataSource={warehouseProducts}
          rowKey="id"
          columns={[
            { title: 'ID', dataIndex: 'id', key: 'id' },
            { title: 'Name', dataIndex: 'name', key: 'name' },
            {
              title: 'Actions',
              render: (_, record) => (
                <Button type="link" danger onClick={() => handleRemoveProduct(record.id)}>
                  Remove
                </Button>
              ),
            },
          ]}
        />
        <div style={{ marginTop: 16 }}>
          <Select
            style={{ width: '100%' }}
            placeholder="Select a product to add"
            onChange={value => setSelectedProduct(value)}
          >
            {availableProducts.map(product => (
              <Select.Option key={product.id} value={product.id}>
                {product.name}
              </Select.Option>
            ))}
          </Select>
          <Button
            type="primary"
            style={{ marginTop: 16 }}
            onClick={handleAddProduct}
            disabled={!selectedProduct}
          >
            Add Product
          </Button>
        </div>
      </Modal>
    </div>
  );
};

export default Warehouses;
