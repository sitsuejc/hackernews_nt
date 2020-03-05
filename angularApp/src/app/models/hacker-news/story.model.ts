import { ItemBase } from './item-base.model';

export interface Story extends ItemBase {
  by: string;
  descendants: number;
  kids: number[];
  score: number;
  time: number;
  title: string;
  type: string;
  url: string | null;
}
