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
  User,
  UserFromJSON,
  UserFromJSONTyped,
  UserToJSON,
  WeekPickStatus,
  WeekPickStatusFromJSON,
  WeekPickStatusFromJSONTyped,
  WeekPickStatusToJSON,
} from "./";

/**
 *
 * @export
 * @interface WeekSummary
 */
export interface WeekSummary {
  /**
   *
   * @type {User}
   * @memberof WeekSummary
   */
  user?: User;
  /**
   *
   * @type {number}
   * @memberof WeekSummary
   */
  score?: number;
  /**
   *
   * @type {number}
   * @memberof WeekSummary
   */
  correctPicks?: number;
  /**
   *
   * @type {number}
   * @memberof WeekSummary
   */
  place?: number;
  /**
   *
   * @type {number}
   * @memberof WeekSummary
   */
  week?: number;
  /**
   *
   * @type {WeekPickStatus}
   * @memberof WeekSummary
   */
  pickStatus?: WeekPickStatus;
}

export function WeekSummaryFromJSON(json: any): WeekSummary {
  return WeekSummaryFromJSONTyped(json, false);
}

export function WeekSummaryFromJSONTyped(
  json: any,
  ignoreDiscriminator: boolean
): WeekSummary {
  if (json === undefined || json === null) {
    return json;
  }
  return {
    user: !exists(json, "user") ? undefined : UserFromJSON(json["user"]),
    score: !exists(json, "score") ? undefined : json["score"],
    correctPicks: !exists(json, "correctPicks")
      ? undefined
      : json["correctPicks"],
    place: !exists(json, "place") ? undefined : json["place"],
    week: !exists(json, "week") ? undefined : json["week"],
    pickStatus: !exists(json, "pickStatus")
      ? undefined
      : WeekPickStatusFromJSON(json["pickStatus"]),
  };
}

export function WeekSummaryToJSON(value?: WeekSummary | null): any {
  if (value === undefined) {
    return undefined;
  }
  if (value === null) {
    return null;
  }
  return {
    user: UserToJSON(value.user),
    score: value.score,
    correctPicks: value.correctPicks,
    place: value.place,
    week: value.week,
    pickStatus: WeekPickStatusToJSON(value.pickStatus),
  };
}
