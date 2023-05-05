import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Producto } from '../shared/models/producto';
import { Marca } from '../shared/models/marca';
import { tipo } from '../shared/models/tipo';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProductos(marcaId?: number, tipoId?: number) {
    let params = new HttpParams();
    
    if(marcaId) params = params.append('BrandId',marcaId);
    if(tipoId) params =  params.append('TypeId',tipoId);

    return this.http.get<Pagination<Producto[]>>(this.baseUrl + 'productos',{params});
  }

  getMarcas(){
    return this.http.get<Marca[]>(this.baseUrl + 'productos/marcas');
  }

  getTipos(){
    return this.http.get<tipo[]>(this.baseUrl + 'productos/tipos');
  }
}
