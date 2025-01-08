import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, Input, message } from 'antd';
import ApiClient from '../Api';

const apiClient = new ApiClient('https://localhost:7173'); // Replace with your API URL

// TypeScript interfaces
interface Product {
  id: string;
  name: string;
  warehouseProducts: any[];
}

const Products: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [form] = Form.useForm();

  const fetchProducts = async () => {
    try {
      const response = await apiClient.getProducts();
      setProducts(response.data as Product[]);
    } catch (error) {
      message.error('Failed to fetch products');
    }
  };

  const handleCreateOrUpdate = async (values: Omit<Product, 'id' | 'warehouseProducts'>) => {
    try {
      if (editingProduct) {
        await apiClient.updateProduct(editingProduct.id, { ...editingProduct, ...values });
        message.success('Product updated successfully');
      } else {
        await apiClient.createProduct({ ...values, warehouseProducts: [] });
        message.success('Product created successfully');
      }
      fetchProducts();
      setIsModalVisible(false);
      form.resetFields();
    } catch (error) {
      message.error('Failed to save product');
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.deleteProduct(id);
      message.success('Product deleted successfully');
      fetchProducts();
    } catch (error) {
      message.error('Failed to delete product');
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  return (
    <div>
      <h2>Products</h2>
      <Button type="primary" onClick={() => setIsModalVisible(true)}>Add Product</Button>
      <Table<Product>
        dataSource={products}
        rowKey="id"
        columns={[
          { title: 'ID', dataIndex: 'id', key: 'id' },
          { title: 'Name', dataIndex: 'name', key: 'name' },
          {
            title: 'Actions',
            render: (_, record) => (
              <>
                <Button
                  type="link"
                  onClick={() => {
                    setEditingProduct(record);
                    setIsModalVisible(true);
                    form.setFieldsValue(record);
                  }}
                >
                  Edit
                </Button>
                <Button type="link" danger onClick={() => handleDelete(record.id)}>Delete</Button>
              </>
            ),
          },
        ]}
      />
      <Modal
        title={editingProduct ? 'Edit Product' : 'Add Product'}
        visible={isModalVisible}
        onCancel={() => {
          setIsModalVisible(false);
          setEditingProduct(null);
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
        </Form>
      </Modal>
    </div>
  );
};

export default Products;
