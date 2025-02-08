import { Injectable } from '@angular/core';
import { Observable, of, map } from 'rxjs';
import { Item } from '../models/user';




import { HttpClient } from '@angular/common/http';  // Importa HttpClient

import { catchError } from 'rxjs/operators';  // Importa catchError para manejar errores


@Injectable({
  providedIn: 'root'
})
export class ItemService {
  private items: Item[] = [];
  private apiUrl = '/api/Items';  

  
  constructor(private http: HttpClient) {}
  

  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.apiUrl).pipe(
      catchError((error: any) => {
         
        console.error('Error fetching items:', error);
        return of([]);  
      })
    );
  }

  addItem(item: Item): Observable<Item> {
    const newItem: Item = {
      ...item,
      id: this.items.length + 1,
      createdAt: new Date()
    };

  
    return this.http.post<Item>(this.apiUrl, newItem).pipe(
   
      map((response: Item) => {
        this.items.push(response);  
        return response;  
      }),
      catchError(error => {
        console.error('Error al agregar el item', error);
        return of(newItem);  
      })
    );
  }


  updateItem(item: Item): Observable<Item | null> {
    const index = this.items.findIndex(i => i.id === item.id);
    if (index !== -1) {
      this.items[index] = { ...item };
      return of(this.items[index]);
    }
    return of(null); 
  }


  deleteItem(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`).pipe(
      map((deleted: boolean) => {
        if (deleted) {
          const index = this.items.findIndex((i) => i.id === id);
          if (index !== -1) {
            this.items.splice(index, 1);
            return true;  
          }
        }
        return false; 
      }),
      catchError((error) => {
        console.error('Error al eliminar el item', error);
        return of(false); 
      })
    );
  }
  

}
