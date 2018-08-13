﻿import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { AbpModule } from '@abp/abp.module';
import { RouterModule } from '@angular/router';

import { AppSessionService } from './session/app-session.service';
import { AppUrlService } from './nav/app-url.service';
import { AppAuthService } from './auth/app-auth.service';
import { AppRouteGuard } from './auth/auth-route-guard';

import { AbpMessage } from './abpmessage/AbpMessage';

@NgModule({
    imports: [
        CommonModule,
        AbpModule,
        RouterModule
    ],
    declarations: [
        
    ],
    exports: [
        
    ]
})
export class AbpSharedModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: AbpSharedModule,
            providers: [
                AppSessionService,
                AppUrlService,
                AppAuthService,
                AppRouteGuard,
                AbpMessage
            ]
        }
    }
}
