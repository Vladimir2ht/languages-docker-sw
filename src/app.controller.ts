import { Controller, Get, Query } from '@nestjs/common';
import { AppService } from './app.service';
import { IPeople, IPlanet, IShip } from "./types";

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get(('get'))
  findAll(@Query() query): Promise<{people: IPeople[], planets: IPlanet[], ships: IShip[]}> {
    console.log(query);
    
    return this.appService.findAll(query.q);
  }
}
