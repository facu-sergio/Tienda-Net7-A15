import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Producto } from 'src/app/shared/models/producto';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  producto?: Producto;
  quantity = 1;
  quantityInBasket = 0;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute,
     private bcService: BreadcrumbService, private basketService: BasketService){
      this.bcService.set('@productDetails', ' ');
     }

  ngOnInit(): void {
    this.loadProuct();
  }
  loadProuct(){
    const id =  this.activateRoute.snapshot.paramMap.get('id');
    if(id) this.shopService.getProducto(+id).subscribe({
      next: producto => {
        this.producto = producto;
        this.bcService.set('@productDetails', producto.nombre);
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
            const item = basket?.items.find(x => x.id === +id);
            if(item){
              this.quantity =  item.quantity;
              this.quantityInBasket = item.quantity;             
            }
          }
        })

      },
      error: err => console.log(err)
    });
  }
  
  updateBasket(){
    if(this.producto){
      if(this.quantity > this.quantityInBasket){
        const itemsToAdd =  this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketService.addItemToBasket(this.producto, itemsToAdd);
        
      }else{
        const itemsToremove = this.quantityInBasket - this.quantity  ;
        this.quantityInBasket -=  itemsToremove;
        this.basketService.removeItemFromBasket(this.producto.id, itemsToremove);
      }
    }
  }

  get buttonText(){
    return this.quantityInBasket === 0 ? 'Agregar al carrito' :  'Actualizar carrito';
  }

  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity(){
    this.quantity--;
  }
}
