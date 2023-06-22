export interface IEntity {
  name: string;
}

export interface IPeople extends IEntity {
	gender: string;
	mass: number | string;
}

export interface IPlanet extends IEntity {
	population: number | string;
  diameter: number | string;
}

export interface IShip extends IEntity {
	length: number | string;
	crew: number | string;
}

export interface IUnnownEntity extends Partial<IPeople>, Partial<IPlanet>, Partial<IShip> {}