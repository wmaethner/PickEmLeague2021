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

/**
 *
 * @export
 * @enum {string}
 */
export enum WeekPickStatus {
  NotPicked = "NotPicked",
  PartiallyPicked = "PartiallyPicked",
  FullyPicked = "FullyPicked",
}

export function WeekPickStatusFromJSON(json: any): WeekPickStatus {
  return WeekPickStatusFromJSONTyped(json, false);
}

export function WeekPickStatusFromJSONTyped(
  json: any,
  ignoreDiscriminator: boolean
): WeekPickStatus {
  return json as WeekPickStatus;
}

export function WeekPickStatusToJSON(value?: WeekPickStatus | null): any {
  return value as any;
}
