import { Component, OnInit,Injector } from '@angular/core';
  import { NzModalRef, NzMessageService } from 'ng-zorro-antd';
  import { _HttpClient } from '@delon/theme';

  import { TelephoneBookServiceProxy, TelephoneBookDto, TelephoneBookListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

  @Component({
    selector: 'books-create',
    templateUrl: './create.component.html',
  })
  export class BooksCreateComponent extends AppComponentBase implements OnInit {
    
    book: TelephoneBookDto = null;
    
    saving: boolean = false;

    constructor(injector: Injector,
      private _telephoneBookService:TelephoneBookServiceProxy,
      private modal: NzModalRef,
      public msgSrv: NzMessageService,
      private subject: NzModalRef,
      public http: _HttpClient
    ) { 
      super(injector);
    }

    ngOnInit(): void {
      
      this.book = new TelephoneBookDto();
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