export class ListMoedaMv
{
    public get description(): string { return this.source + " - " + this.name; }

    constructor(public source: string, 
                public name: string){}
}