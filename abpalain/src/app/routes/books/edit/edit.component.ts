import { Component, OnInit,Injector,Input } from '@angular/core';
  import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
  import { _HttpClient } from '@delon/theme';

  
  import { TelephoneBookServiceProxy, TelephoneBookDto, TelephoneBookListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';


  @Component({
    selector: 'books-edit',
    templateUrl: './edit.component.html',
  })
  export class BooksEditComponent extends AppComponentBase  implements OnInit {
    
    book: TelephoneBookDto = null;
    
    @Input()
    bookId:string = null;

    saving: boolean = false;

    constructor(injector: Injector,
      private _telephoneBookService:TelephoneBookServiceProxy,
      private modal: NzModalRef,
      public msgSrv: NzMessageService,
      private subject: NzModalRef,
      public http: _HttpClient
    ) { 
      super(injector);
      this.book = new TelephoneBookDto();
    }

    ngOnInit(): void {
      
     // this.book = new TelephoneBookDto();
     this._telephoneBookService.getForEdit(this.bookId)
     .subscribe(
     (result) => {
         this.book = result;
     });
      // this.http.get(`/user/${this.record.id}`).subscribe(res => this.i = res);
    }

    save(): void {

      this.saving = true;
  
      this._telephoneBookService.createOrUpdate(this.book)
        .pipe(finalize(() => {
          this.saving = false;
        }))
        .subscribe((res) => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close();
        });
    }

    close() {
      this.subject.destroy();
    }
  }
