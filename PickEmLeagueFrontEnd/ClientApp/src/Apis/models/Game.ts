/* tslint:disable */
/* eslint-disable */
/**
 * PickEmLeague
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 *
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from "../runtime";
import {
  GameResult,
  GameResultFromJSON,
  GameResultFromJSONTyped,
  GameResultToJSON,
  Team,
  TeamFromJSON,
  TeamFromJSONTyped,
  TeamToJSON,
} from "./";

/**
 *
 * @export
 * @interface Game
 */
export interface Game {
  /**
   *
   * @type {number}
   * @memberof Game
   */
  week?: number;
  /**
   *
   * @type {Date}
   * @memberof Game
   */
  gameTime?: Date;
  /**
   *
   * @type {string}
   * @memberof Game
   */
  gameTimeString?: string | null;
  /**
   *
   * @type {GameResult}
   * @memberof Game
   */
  gameResult?: GameResult;
  /**
   *
   * @type {Team}
   * @memberof Game
   */
  homeTeam?: Team;
  /**
   *
   * @type {Team}
   * @memberof Game
   */
  awayTeam?: Team;
  /**
   *
   * @type {number}
   * @memberof Game
   */
  homeTeamId?: number;
  /**
   *
   * @type {number}
   * @memberof Game
   */
  awayTeamId?: number;
  /**
   *
   * @type {number}
   * @memberof Game
   */
  id?: number;
}

export function GameFromJSON(json: any): Game {
  return GameFromJSONTyped(json, false);
}

export function GameFromJSONTyped(
  json: any,
  ignoreDiscriminator: boolean
): Game {
  if (json === undefined || json === null) {
    return json;
  }
  return {
    week: !exists(json, "week") ? undefined : json["week"],
    gameTime: !exists(json, "gameTime")
      ? undefined
      : new Date(json["gameTime"]),
    gameTimeString: !exists(json, "gameTimeString")
      ? undefined
      : json["gameTimeString"],
    gameResult: !exists(json, "gameResult")
      ? undefined
      : GameResultFromJSON(json["gameResult"]),
    homeTeam: !exists(json, "homeTeam")
      ? undefined
      : TeamFromJSON(json["homeTeam"]),
    awayTeam: !exists(json, "awayTeam")
      ? undefined
      : TeamFromJSON(json["awayTeam"]),
    homeTeamId: !exists(json, "homeTeamId") ? undefined : json["homeTeamId"],
    awayTeamId: !exists(json, "awayTeamId") ? undefined : json["awayTeamId"],
    id: !exists(json, "id") ? undefined : json["id"],
  };
}

export function GameToJSON(value?: Game | null): any {
  if (value === undefined) {
    return undefined;
  }
  if (value === null) {
    return null;
  }
  return {
    week: value.week,
    gameTime:
      value.gameTime === undefined ? undefined : value.gameTime.toISOString(),
    gameTimeString: value.gameTimeString,
    gameResult: GameResultToJSON(value.gameResult),
    homeTeam: TeamToJSON(value.homeTeam),
    awayTeam: TeamToJSON(value.awayTeam),
    homeTeamId: value.homeTeamId,
    awayTeamId: value.awayTeamId,
    id: value.id,
  };
}
