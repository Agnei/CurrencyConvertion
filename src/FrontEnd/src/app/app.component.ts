import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListMoedaMv } from './model/list-moeda-mv'
import { ConverterMoedaCommand } from './model/converter-moeda-command'
import { ConverterMoedaService } from './services/converter-moeda-service'

@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html'
})
export class AppComponent implements OnInit {

  groupApp: FormGroup;

  get getSourceValue() { return this.groupApp.get('source').value; }
  get getTargetValue() { return this.groupApp.get('target').value; }
  get getSourceAmountValue() { return this.groupApp.get('sourceAmount').value; }
  get getTargetAmountValue() { return this.groupApp.get('targetAmount').value; }

  // setSourceAmountValue(data: number) { this.groupApp.get('sourceAmount').value = data; }
  // setTargetAmountValue(data: number) { this.groupApp.get('targetAmount').value = data; }

  public cotacao: number = 0.00000;

  public sourceAmount: number = 0 * this.cotacao; 
  public targetAmount: number = 0 * this.cotacao;

  constructor(private fb: FormBuilder, 
              private service: ConverterMoedaService) { }

  ngOnInit() {
        this.groupApp = this.fb.group({
            source: ['', [Validators.required]],
            target: ['', [Validators.required]],
            sourceAmount: [0.0000], 
            targetAmount: [0.0000] 
          });    
    }


    public selectionMoedaChange(event: ListMoedaMv): void
    {
        if(!this.getSourceValue.source || !this.getTargetValue.source) return;

        this.getCotacaoMoeda(this.getSourceValue.source, this.getTargetValue.source, 1); 
    }

    public getCotacaoMoeda(from: string, to: string, amount: number)
    {
        var command = new ConverterMoedaCommand(from, to, amount);
        return this.service.postConverterMoeda(command).subscribe(x => 
        { 
          this.cotacao = x.data.result;
          this.groupApp.get('sourceAmount').patchValue(1);
          this.onAmountchChange(1, "source");
        });
    }

    public onAmountchChange(value: number, origin: string)
    {
      if(!this.cotacao || this.cotacao === 0) return; 

      switch(origin) { 
        case "source": { 
           this.groupApp.get('targetAmount').patchValue(value * this.cotacao); 
           break; 
        } 
        case "target": { 
           this.groupApp.get('sourceAmount').patchValue(value / this.cotacao)
           break; 
        } 
      } 
    }
}
