import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, Input, message } from 'antd';
import ApiClient from '../Api';

const apiClient = new ApiClient('https://localhost:7173'); // Replace with your API URL

// TypeScript interfaces
interface Warehouse {
  id: string;
  name: string;
  location: string;
  warehouseProducts: any[];
}

const Warehouses: React.FC = () => {
  const [warehouses, setWarehouses] = useState<Warehouse[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingWarehouse, setEditingWarehouse] = useState<Warehouse | null>(null);
  const [form] = Form.useForm();

  const fetchWarehouses = async () => {
    try {
      const response = await apiClient.getWarehouses();
      setWarehouses(response.data as Warehouse[]);
    } catch (error) {
      message.error('Failed to fetch warehouses');
    }
  };

  const handleCreateOrUpdate = async (values: Omit<Warehouse, 'id' | 'warehouseProducts'>) => {
    try {
      if (editingWarehouse) {
        await apiClient.updateWarehouse(editingWarehouse.id, { ...editingWarehouse, ...values });
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

  useEffect(() => {
    fetchWarehouses();
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
    </div>
  );
};

export default Warehouses;
