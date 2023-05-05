import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductoItemComponent } from './producto-item/producto-item.component';



@NgModule({
  declarations: [
    ShopComponent,
    ProductoItemComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ShopComponent
  ]
})
export class ShopModule { }
