import { Component, 
         ComponentFactoryResolver, 
         ComponentRef,
         Directive,
         Input,
         Output, 
         OnInit,
         EventEmitter, 
         ViewContainerRef } 
from '@angular/core';

import { FormGroup } 
from '@angular/forms';

import { Observable, empty, of } 
from 'rxjs';

import { switchMap, 
         debounceTime, 
         tap, 
         finalize, 
         map } 
from 'rxjs/operators';

import { ConverterMoedaCommand } 
from '../model/converter-moeda-command';

import {ListMoedaMv}
from '../model/list-moeda-mv'

import { ConverterMoedaService } 
from '../services/converter-moeda-service'

@Component({
  selector: 'app-selecao-moeda',
  templateUrl: './selecao-moeda.component.html'
})
export class SelecaoMoedaComponent implements OnInit {

  @Input() placeholder: string;
  @Input() label: string;
  @Input() name: string;
  @Input() group: FormGroup;

  @Output() itemSelectChanged = new EventEmitter<any>();

  isLoading = false;
  moedas: ListMoedaMv[] = [];
  notFound = new ListMoedaMv("Item not found", "Try Againg");

  displayFn(list: ListMoedaMv) {
    if (list) { return list.description; }
  }

  constructor(private service: ConverterMoedaService) { }

  ngOnInit() {
    

    this.group.get(this.name).valueChanges
              .pipe(debounceTime(500),
                    tap(() => this.isLoading = true),
                    switchMap(search =>  this.getMoedas(search)
                                             .pipe(finalize(() => this.isLoading = false))))
              .subscribe((ms) => 
                         {  
                            var search = this.group.get(this.name).value;
                            var buscarMoeda: ListMoedaMv[] = [];
                            this.moedas = [];

                            //prevent reload
                            if(search instanceof  ListMoedaMv) return;

                            ms.forEach(moeda => {
                              buscarMoeda.push(new ListMoedaMv(moeda.source, moeda.name)); 
                            });

                            var filter = buscarMoeda.filter(f => f.description.toLowerCase().indexOf((search) ? search.toLowerCase() : null)  >= 0); 

                            (!filter || filter.length <= 0) ? this.moedas.push(this.notFound) : this.moedas = filter;
                         });
  }

  getMoedas(search: string): Observable<ListMoedaMv[]> {    
    return (!search) ? 
            empty() : 
            this.service.getMoedaSuportadas();
  }

  itemSelectionChanged(eventArg: ListMoedaMv)
  {
    // const enquadramento = <AtividadeProfisionalViewModel>this.atividadeSearchForm.get('atividadeSearchInput').value
    this.itemSelectChanged.emit(eventArg);    
  }
}
