import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { Producto } from 'src/app/shared/models/producto';

@Component({
  selector: 'app-producto-item',
  templateUrl: './producto-item.component.html',
  styleUrls: ['./producto-item.component.scss']
})
export class ProductoItemComponent {
@Input() producto?: Producto;
 constructor(private basketService: BasketService){}

 addItemToBasket(){
  this.producto  && this.basketService.addItemToBasket(this.producto);
 }
}
