import { HttpService } from '@nestjs/axios';
import { Injectable } from '@nestjs/common';
import { IPeople, IPlanet, IShip, IUnnownEntity } from "./types";

@Injectable()
export class AppService {
  constructor(private readonly httpService: HttpService) {}
  
  async findAll(query: string): Promise<{people: IPeople[], planets: IPlanet[], ships: IShip[]}> {
    const [people, planets, ships] = await Promise.all([
      this.searchEntities('people', query),
      this.searchEntities('planets', query),
      this.searchEntities('starships', query),
    ]);

    return {
      people: people.map(el => ({name: el.name, gender: el.gender, mass: el.mass})),
      planets: planets.map(el => ({name: el.name, population: el.population, diameter: el.diameter})),
      ships: ships.map(el => ({name: el.name, length: el.length, crew: el.crew})),
    };
  }

  private async searchEntities(
    entity: string,
    query: string,
  ): Promise<IUnnownEntity[]> {
    const url = `https://swapi.dev/api/${entity}/`;

    let results: IUnnownEntity[] = [];
    let nextPageUrl = url;

    while (nextPageUrl) {
      const response = await this.httpService.get(nextPageUrl).toPromise();

      const entities = response.data.results.filter((e: IUnnownEntity) =>
        e.name.toLowerCase().includes(query.toLowerCase()),
      );

      results = [...results, ...entities];
      nextPageUrl = response.data.next;
    }

    return results;
  }
}
