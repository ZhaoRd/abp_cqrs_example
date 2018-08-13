import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { BooksRoutingModule } from './books-routing.module';
import { BooksListComponent } from './list/list.component';
import { BooksCreateComponent } from './create/create.component';
import { BooksEditComponent } from './edit/edit.component';

const COMPONENTS = [
  BooksListComponent];
const COMPONENTS_NOROUNT = [
  BooksCreateComponent,
  BooksEditComponent];

@NgModule({
  imports: [
    SharedModule,
    BooksRoutingModule
  ],
  declarations: [
    ...COMPONENTS,
    ...COMPONENTS_NOROUNT
  ],
  entryComponents: COMPONENTS_NOROUNT
})
export class BooksModule { }