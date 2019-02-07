import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { Router } from "@angular/router";

@Injectable()
export class CustomErrorHandler implements ErrorHandler {
    constructor(private readonly injector: Injector) { }

    handleError(error: any) {
        console.log("Deu ruim tío, quebrou o bagulho! Foi mau aeeeee.");
        console.log(error);
    }
}
