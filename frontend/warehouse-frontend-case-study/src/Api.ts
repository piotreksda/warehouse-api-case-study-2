import axios, { AxiosInstance } from 'axios';

class ApiClient {
  private api: AxiosInstance;

  constructor(baseURL: string) {
    this.api = axios.create({
      baseURL,
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  // User endpoints
  async register(user: { username: string; password: string }) {
    return this.api.post('/register', user);
  }

  async login(user: { username: string; password: string }) {
    return this.api.post('/login', user);
  }

  // Products endpoints
  async getProducts() {
    return this.api.get('/products');
  }

  async createProduct(product: { name: string; warehouseProducts: any[] }) {
    return this.api.post('/products', product);
  }

  async getProductById(id: string) {
    return this.api.get(`/products/${id}`);
  }

  async updateProduct(id: string, product: { id: string; name: string; warehouseProducts: any[] }) {
    return this.api.put(`/products/${id}`, product);
  }

  async deleteProduct(id: string) {
    return this.api.delete(`/products/${id}`);
  }

  // Warehouses endpoints
  async getWarehouses() {
    return this.api.get('/warehouses');
  }

  async createWarehouse(warehouse: { name: string; location: string; warehouseProducts: any[] }) {
    return this.api.post('/warehouses', warehouse);
  }

  async getWarehouseById(id: string) {
    return this.api.get(`/warehouses/${id}`);
  }

  async updateWarehouse(id: string, warehouse: { id: string; name: string; location: string; warehouseProducts: any[] }) {
    return this.api.put(`/warehouses/${id}`, warehouse);
  }

  async deleteWarehouse(id: string) {
    return this.api.delete(`/warehouses/${id}`);
  }

  async addProductToWarehouse(warehouseId: string, productId: string) {
    return this.api.put(`/warehouses/${warehouseId}/products/${productId}/add`);
  }

  async removeProductFromWarehouse(warehouseId: string, productId: string) {
    return this.api.put(`/warehouses/${warehouseId}/products/${productId}/remove`);
  }
}

// Usage Example
const client = new ApiClient('https://localhost:7173'); // Use the appropriate base URL

export default ApiClient;

// TypeScript interfaces for Products and Warehouses
export interface Product {
    id: string;
    name: string;
    warehouseProducts: any[];
  }
  
export interface Warehouse {
    id: string;
    name: string;
    location: string;
    warehouseProducts: any[];
  }