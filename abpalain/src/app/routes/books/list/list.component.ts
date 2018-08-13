import { Component, OnInit, Injector } from '@angular/core';
import { _HttpClient, ModalHelper } from '@delon/theme';
import { SimpleTableColumn, SimpleTableComponent } from '@delon/abc';
import { SFSchema } from '@delon/form';
import { finalize } from 'rxjs/operators';

import { AppComponentBase } from '@shared/app-component-base';

import { TelephoneBookServiceProxy, TelephoneBookDto, TelephoneBookListDto } from '@shared/service-proxies/service-proxies';

import { BooksCreateComponent } from './../create/create.component'
import { BooksEditComponent } from './../edit/edit.component'

@Component({
  selector: 'books-list',
  templateUrl: './list.component.html',
})
export class BooksListComponent extends AppComponentBase  implements OnInit {

    params: any = {};
    
    list = [];
    loading = false;

    constructor( injector: Injector,private http: _HttpClient, private modal: ModalHelper,
      private _telephoneBookService:TelephoneBookServiceProxy) { 
        super(injector);
      }

    ngOnInit() { 
      this.loading = true;
      this._telephoneBookService
          .getAllTelephoneBookList()
          .pipe(finalize(
            ()=>{
              this.loading = false;
            }
          ))
          .subscribe(res=>{
            this.list = res;
          })
          ;

    }

    edit(id: string): void {
      this.modal.static(BooksEditComponent, {
        bookId: id
      }).subscribe(res => {
        this.ngOnInit();
      });
    }

    add() {
      
       this.modal
         .static(BooksCreateComponent, { id: null })
         .subscribe(() => this.ngOnInit());
    }

    delete(book: TelephoneBookListDto): void {
      abp.message.confirm(
        "删除通讯录 '" + book.name + "'?"
      ).then((result: boolean) => {
        console.log(result);
        if (result) {
          this._telephoneBookService.delete(book.id)
            .pipe(finalize(() => {
              abp.notify.info("删除通讯录: " + book.name);
              this.ngOnInit();
            }))
            .subscribe(() => { });
        }
      });
    }

}
