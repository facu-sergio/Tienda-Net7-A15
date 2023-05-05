import { Component, Input } from '@angular/core';
import { Producto } from 'src/app/shared/models/producto';

@Component({
  selector: 'app-producto-item',
  templateUrl: './producto-item.component.html',
  styleUrls: ['./producto-item.component.scss']
})
export class ProductoItemComponent {
@Input() producto?: Producto;
}
