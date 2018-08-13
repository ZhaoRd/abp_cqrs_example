import { NgModule, LOCALE_ID, APP_INITIALIZER, Injector } from '@angular/core';
import {
  HttpClient,
  HTTP_INTERCEPTORS,
  HttpClientModule,
} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { DelonModule } from './delon.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { AppComponent } from './app.component';
import { RoutesModule } from './routes/routes.module';
import { LayoutModule } from './layout/layout.module';
import { StartupService } from '@core/startup/startup.service';
import { DefaultInterceptor } from '@core/net/default.interceptor';
import { SimpleInterceptor } from '@delon/auth';
// angular i18n
import { registerLocaleData } from '@angular/common';
import localeZh from '@angular/common/locales/zh';
registerLocaleData(localeZh);
// i18n
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { MenuService, ALAIN_I18N_TOKEN } from '@delon/theme';
import { I18NService } from '@core/i18n/i18n.service';

// third
import { UEditorModule } from 'ngx-ueditor';
import { NgxTinymceModule } from 'ngx-tinymce';
// @delon/form: JSON Schema form
import { JsonSchemaModule } from '@shared/json-schema/json-schema.module';

import { AbpModule } from '@abp/abp.module';
import { AbpHttpInterceptor } from '@abp/abpHttpInterceptor';

import { AppPreBootstrap } from './../AppPreBootstrap';
import { AppSessionService } from '@shared/session/app-session.service';
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { AbpSharedModule } from '@shared/abpshared.module';
import { AppConsts } from '@shared/AppConsts';

import { AbpMessage } from '@shared/abpmessage/AbpMessage';

import * as _ from 'lodash';

// 加载i18n语言文件
export function I18nHttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, `assets/tmp/i18n/`, '.json');
}




export function StartupServiceFactory(
  injector: Injector,
  startupService: StartupService,
): Function {
  return () =>
    startupService
      .load().then(() => {

        // 初始化消息类通知
        const abpMessage = injector.get(AbpMessage);
        abpMessage.init();

        // 初始化abp
        return new Promise<boolean>((resolve, reject) => {
          AppPreBootstrap.run(() => {
            abp.event.trigger('abp.dynamicScriptsInitialized');

            const appSessionService: AppSessionService = injector.get(
              AppSessionService,
            );

            appSessionService.init().then(
              result => {
                resolve(result);
              },
              err => {
                reject(err);
              },
            );
          });
        });
      })
      .then(() => {

        /**
        * 根据权限修改菜单是否显示
        * @param menus 
        */
        function checkMenuPerssion(menus) {
          _.forEach(menus, (item) => {

            item.hide = item.permissions && !abp.auth.isGranted(item.permissions);

            if (item.children != undefined && item.children.length > 0) {
              checkMenuPerssion(item.children);
            }
          });
        }

        
        // 验证菜单权限
        var menuService: MenuService = injector.get(MenuService);
        var menus = menuService.menus;
        
        checkMenuPerssion(menus);

        // 需要重新设置菜单
        menuService.clear();
        menuService.add(menus);

      });
}


export function getRemoteServiceBaseUrl(): string {
  return AppConsts.remoteServiceBaseUrl;
}

export function getCurrentLanguage(): string {
  return abp.localization.currentLanguage.name;
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    DelonModule.forRoot(),
    AbpSharedModule.forRoot(),
    AbpSharedModule,
    AbpModule,
    ServiceProxyModule,
    CoreModule,
    SharedModule,
    LayoutModule,
    RoutesModule,
    // i18n
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: I18nHttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    // thirds
    UEditorModule.forRoot({
      // **注：** 建议使用本地路径；以下为了减少 ng-alain 脚手架的包体大小引用了CDN，可能会有部分功能受影响
      js: [
        `//apps.bdimg.com/libs/ueditor/1.4.3.1/ueditor.config.js`,
        `//apps.bdimg.com/libs/ueditor/1.4.3.1/ueditor.all.min.js`,
      ],
      options: {
        UEDITOR_HOME_URL: `//apps.bdimg.com/libs/ueditor/1.4.3.1/`,
      },
    }),
    NgxTinymceModule.forRoot({
      baseURL: '//cdn.bootcss.com/tinymce/4.7.4/',
    }),
    // JSON-Schema form
    JsonSchemaModule,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'zh-Hans' },
    /*
    { provide: HTTP_INTERCEPTORS, useClass: SimpleInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true },
    */
    { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true },
    { provide: ALAIN_I18N_TOKEN, useClass: I18NService, multi: false },
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl },
    StartupService,
    {
      provide: APP_INITIALIZER,
      useFactory: StartupServiceFactory,
      deps: [Injector, StartupService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
