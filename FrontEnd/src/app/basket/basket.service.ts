import { Injectable } from '@angular/core';
import { BehaviorSubject, retry } from 'rxjs';
import { enviroment } from 'src/environments/environment';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Producto } from '../shared/models/producto';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = enviroment.apiUrl;
  private basketSource =  new BehaviorSubject <Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject <BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http:HttpClient) { }

  //obtener carrito desde api
  getBasket(id: string){
    return this.http.get<Basket>(this.baseUrl + 'basket?id=' + id).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      } 
    })
  }

  //enviar carrito a la api
  setBasket(basket: Basket){
    return this.http.post<Basket>(this.baseUrl + 'basket', basket).subscribe({
      next:  basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      } 
    })
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  //agregar item a carrito y si no tenemos carrito lo crea
  addItemToBasket(item: Producto | BasketItem, quantity = 1 ){
    if(this.isProduct(item)) item = this.mapProductItemToBasketItem(item) 
    const basket =  this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }

  //Eliminar item del carrito
 removeItemFromBasket(id: number, quantity = 1){
  const basket = this.getCurrentBasketValue();
  if(!basket)return;
  const item =  basket.items.find(x => x.id == id);
  if(item){
    item.quantity -= quantity;
    if(item.quantity === 0){
      basket.items =  basket.items.filter(x => x.id !== id);
    }
  }
  if( basket.items.length > 0 ) this.setBasket(basket);
  else this.deleteBasket(basket);
 }

//elimina el carrito de la api y localstorage
  deleteBasket(basket: Basket) {
    return this.http.delete<Basket>(this.baseUrl + 'basket?id=' + basket.id ).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
    })
  }
  //verifica si el producto existe en el carrito y aumenta su cantidad o agrega con su cantidad correspondiente
 private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const item =  items.find(x => x.id == itemToAdd.id);
    if(item) item.quantity += quantity;
    else{
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

//crea un carrito y lo guarda en localstorage del navegador
 private createBasket(): Basket{
    const basket =  new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  //funcion para mapear un producto con un item del carrito
  private mapProductItemToBasketItem(item: Producto): BasketItem {
    return {
      id: item.id,
      productName: item.nombre,
      price: item.precio,
      quantity: 0,
      pictureUrl: item.fotoUrl,
      brand: item.marca,
      type: item.tipo
    }
  }

  private calculateTotals(){
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const shipping = 0;
    const subTotal = basket.items.reduce((a, b) => (b.price * b.quantity)+ a, 0);
    const total = subTotal + shipping;
    this.basketTotalSource.next({shipping,total,subTotal})
  }

  private isProduct(item: Producto | BasketItem): item is Producto{
    return (item as Producto).marca !== undefined;
  }
}
