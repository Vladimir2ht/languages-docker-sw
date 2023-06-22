import { Controller, Get, Query } from '@nestjs/common';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get(('get'))
  findAll(@Query() query) {
    console.log(query);
    
    return this.appService.findAll(query.q);
  }
}
