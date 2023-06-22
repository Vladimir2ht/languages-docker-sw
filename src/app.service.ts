import { HttpService } from '@nestjs/axios';
import { Injectable } from '@nestjs/common';

@Injectable()
export class AppService {
  constructor(private readonly httpService: HttpService) {}
  
  // findAll(query: string) {

  async findAll(query: string): Promise<{people: object[], planets: object[], ships: object[]}> {
    const [people, planets, ships] = await Promise.all([
      this.searchEntities('people', query),
      this.searchEntities('planets', query),
      this.searchEntities('starships', query),
    ]);

    return {
      people: people.map(el => ({name: el.name, gender: el.gender, weight: el.mass})),
      planets: planets.map(el => ({name: el.name, population: el.population, d: el.diameter})),
      ships: ships.map(el => ({name: el.name, length: el.length, crew: el.crew})),
    };
  }

  private async searchEntities(
    entity: string,
    query: string,
  ): Promise<{name: string, gender?: string, mass?, population?, diameter?, length?, crew?}[]> {
    const url = `https://swapi.dev/api/${entity}/`;

    let results: any[] = [];
    let nextPageUrl = url;

    while (nextPageUrl) {
      const response = await this.httpService
        .get(nextPageUrl)
        .toPromise();

      const entities = response.data.results.filter((e: any) =>
        e.name.toLowerCase().includes(query.toLowerCase()),
      );

      results = [...results, ...entities];
      nextPageUrl = response.data.next;
    }

    return results;
  }
}
