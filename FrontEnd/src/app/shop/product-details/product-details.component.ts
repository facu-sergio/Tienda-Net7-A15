import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Producto } from 'src/app/shared/models/producto';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute){}
  producto?: Producto;
  ngOnInit(): void {
    this.loadProuct();
  }
  loadProuct(){
    const id =  this.activateRoute.snapshot.paramMap.get('id');
    if(id) this.shopService.getProducto(+id).subscribe({
      next: producto => this.producto = producto,
      error: err => console.log(err)
    });
  }
}
