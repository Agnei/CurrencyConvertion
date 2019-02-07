//#region Imports
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { ConverterMoedaCommand } from '../model/converter-moeda-command'
import { ListMoedaMv } from '../model/list-moeda-mv'
import { ConverterMoedaModelView } from '../model/converter-moeda-mv'
import { environment } from "../../environments/environment";
import {
  map,
  debounceTime,
  distinctUntilChanged,
  switchMap,
  tap
} from "rxjs/operators";

@Injectable({
    providedIn: 'root'
})
export class ConverterMoedaService  {

    private URLs = {
        getMoedaSuportadas: 'assets/moedas.json',
        postConverterMoeda: 'api/v1/ConverterMoeda/converter'
    };

    constructor(private http: HttpClient) {}

    public getMoedaSuportadas(): Observable<ListMoedaMv[]> {
        return this.http.get<ListMoedaMv[]>(this.URLs.getMoedaSuportadas);
    }

    public postConverterMoeda(command: ConverterMoedaCommand): Observable<any> {
        return this.http.put<any>(this.preperUrl(this.URLs.postConverterMoeda), command);
    }

    private preperUrl(url: string): string
    {
        return environment.API + "/" + url;
    }
}