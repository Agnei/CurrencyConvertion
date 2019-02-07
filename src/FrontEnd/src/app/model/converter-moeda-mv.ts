export class ConverterMoedaModelView
{
    constructor(
        public from: string, 
        public to: string,
        public quote: number,
        public amount: number,
        public result: number ){}
}