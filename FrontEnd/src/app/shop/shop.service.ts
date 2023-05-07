import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Producto } from '../shared/models/producto';
import { Marca } from '../shared/models/marca';
import { tipo } from '../shared/models/tipo';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProductos(shopParams: ShopParams) {
    let params = new HttpParams();
    
    if(shopParams.marcaId > 0) params = params.append('BrandId',shopParams.marcaId);
    if(shopParams.tipoId > 0) params =  params.append('TypeId',shopParams.tipoId);
    params =  params.append('Sort',shopParams.sort);
    params =  params.append('pageIndex',shopParams.pageNumber);
    params =  params.append('pageSize',shopParams.pageSize);
    return this.http.get<Pagination<Producto[]>>(this.baseUrl + 'productos',{params});
  }

  getMarcas(){
    return this.http.get<Marca[]>(this.baseUrl + 'productos/marcas');
  }

  getTipos(){
    return this.http.get<tipo[]>(this.baseUrl + 'productos/tipos');
  }
}
