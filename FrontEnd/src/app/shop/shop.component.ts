import { Component, OnInit } from '@angular/core';
import { Producto } from '../shared/models/producto';
import { ShopService } from './shop.service';
import { Marca } from '../shared/models/marca';
import { tipo } from '../shared/models/tipo';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{
  productos: Producto[] = [];
  tipos: tipo[] = [];
  marcas: Marca[] = [];
  marcaIdSelected = 0;
  tipoIdSelected = 0;

  constructor(private shopService:ShopService){}
  ngOnInit(): void {
   this.getProductos();
   this.getTipos();
   this.getMarcas();
  }

  getProductos(){
    this.shopService.getProductos(this.marcaIdSelected,this.tipoIdSelected).subscribe({
      next: response => this.productos = response.data,
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
    this.marcaIdSelected =  marcaId;
    this.getProductos();
  }

  onTipoSelected(tipoId: number){
    this.tipoIdSelected = tipoId;
    this.getProductos();
  }
}
