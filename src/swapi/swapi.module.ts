import { Module } from '@nestjs/common';
import { SwapiController } from './swapi/swapi.controller';
import { SwapiService } from './swapi/swapi.service';

@Module({
  controllers: [SwapiController],
  providers: [SwapiService]
})
export class SwapiModule {}
