import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ItemService } from '../../services/item.service';
import { AuthService } from '../../services/auth.service';
import { Item } from '../../models/user';

@Component({
  selector: 'app-items',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="items-container">
      <div class="header">
        <h2>Items Management</h2>
        <button (click)="logout()" class="logout-btn">Logout</button>
      </div>

      <div class="add-item-form">
        <h3>{{ editingItem ? 'Edit Item' : 'Add New Item' }}</h3>
        <form (ngSubmit)="onSubmit()">
          <div class="form-group">
            <input
              type="text"
              [(ngModel)]="currentItem.name"
              name="name"
              placeholder="Item name"
              required
            />
          </div>
          <div class="form-group">
            <input
              type="text"
              [(ngModel)]="currentItem.description"
              name="description"
              placeholder="Description"
              required
            />
          </div>
          <button type="submit">{{ editingItem ? 'Update' : 'Add' }}</button>
          <button *ngIf="editingItem" type="button" (click)="cancelEdit()">Cancel</button>
        </form>
      </div>

      <div class="items-list">
        <h3>Items List</h3>
        <div *ngFor="let item of items" class="item-card">
          <div class="item-content">
            <h4>{{ item.name }}</h4>
            <p>{{ item.description }}</p>
            <small>Created: {{ item.createdAt | date }}</small>
          </div>
          <div class="item-actions">
            <button (click)="editItem(item)">Edit</button>
            <button *ngIf="item.id" (click)="deleteItem(item.id)" class="delete-btn">Delete</button>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .items-container {
      max-width: 800px;
      margin: 20px auto;
      padding: 20px;
    }
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 20px;
    }
    .logout-btn {
      background-color: #dc3545;
    }
    .add-item-form {
      margin-bottom: 30px;
      padding: 20px;
      border: 1px solid #ccc;
      border-radius: 5px;
    }
    .form-group {
      margin-bottom: 15px;
    }
    .form-group input {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }
    button {
      padding: 8px 15px;
      margin-right: 10px;
      background-color: #007bff;
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    button:hover {
      background-color: #0056b3;
    }
    .delete-btn {
      background-color: #dc3545;
    }
    .delete-btn:hover {
      background-color: #c82333;
    }
    .item-card {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 15px;
      margin-bottom: 10px;
      border: 1px solid #ddd;
      border-radius: 4px;
    }
    .item-content {
      flex-grow: 1;
    }
    .item-content h4 {
      margin: 0 0 5px 0;
    }
    .item-content p {
      margin: 0 0 5px 0;
      color: #666;
    }
    .item-content small {
      color: #999;
    }
    .item-actions {
      display: flex;
      gap: 10px;
    }
  `]
})
export class ItemsComponent implements OnInit {
  items: Item[] = [];
  currentItem: Item = { name: '', description: '' };
  editingItem: Item | null = null;

  constructor(
    private itemService: ItemService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {

    
    this.loadItems();
  }

  loadItems() {
    this.itemService.getItems().subscribe(items => {
      this.items = items;
    });
  }

  onSubmit() {
    if (this.editingItem) {
      this.itemService.updateItem({
        ...this.currentItem,
        id: this.editingItem.id
      }).subscribe(() => {
        this.loadItems();
        this.resetForm();
      });
    } else {
      this.itemService.addItem(this.currentItem).subscribe(() => {
        this.loadItems();
        this.resetForm();
      });
    }
  }

  editItem(item: Item) {
    this.editingItem = item;
    this.currentItem = { ...item };
  }

  deleteItem(id: number) {
    if (confirm('Are you sure you want to delete this item?')) {
      this.itemService.deleteItem(id).subscribe(() => {
        this.loadItems();
      });
    }
  }

  cancelEdit() {
    this.resetForm();
  }

  resetForm() {
    this.currentItem = { name: '', description: '' };
    this.editingItem = null;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
