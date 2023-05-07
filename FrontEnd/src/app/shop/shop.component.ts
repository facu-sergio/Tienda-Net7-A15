import { Component, OnInit } from '@angular/core';
import { Producto } from '../shared/models/producto';
import { ShopService } from './shop.service';
import { Marca } from '../shared/models/marca';
import { tipo } from '../shared/models/tipo';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{
  productos: Producto[] = [];
  tipos: tipo[] = [];
  marcas: Marca[] = [];
  shopParams =  new ShopParams();
  sortOptions = [
    {'name':'Alfabetico','value':'name'},
    {'name':'Menor precio','value':'priceAsc'},
    {'name':'Mayor precio','value':'priceDesc'}
];
  totalCount = 0;
  constructor(private shopService:ShopService){}
  ngOnInit(): void {
   this.getProductos();
   this.getTipos();
   this.getMarcas();
  }

  getProductos(){
    this.shopService.getProductos(this.shopParams).subscribe({
      next: response => {
        this.productos = response.data;
        this.shopParams.pageSize = response.pageSize;
        this.shopParams.pageNumber = response.pageIndex;
        this.totalCount =  response.count;
      },
      error: error => console.log(error)
    })
  }

  getTipos(){
    this.shopService.getTipos().subscribe({
      next: response => this.tipos = [{id: 0, nombre:'All'}, ...response],
      error: error => console.log(error)
    })
  }

  getMarcas(){
    this.shopService.getMarcas().subscribe({
      next: response => this.marcas = [{id: 0, nombre:'All'}, ...response],
      error: error => console.log (error)
    })
  }

  onMarcaSelected(marcaId: number){
    this.shopParams.marcaId =  marcaId;
    this.getProductos();
  }

  onTipoSelected(tipoId: number){
    this.shopParams.tipoId = tipoId;
    this.getProductos();
  }

  onSortSelected($event:any){
    this.shopParams.sort = $event.target.value;
    this.getProductos();
  }

  onPageChanged(event: any ){
    if(this.shopParams.pageNumber !== event.page){
      this.shopParams.pageNumber = event.page;
      this.getProductos();
    }
  }
}
