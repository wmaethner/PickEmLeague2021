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
import { User, UserFromJSON, UserFromJSONTyped, UserToJSON } from "./";

/**
 *
 * @export
 * @interface LoginResponse
 */
export interface LoginResponse {
  /**
   *
   * @type {boolean}
   * @memberof LoginResponse
   */
  loggedIn?: boolean;
  /**
   *
   * @type {User}
   * @memberof LoginResponse
   */
  user?: User;
}

export function LoginResponseFromJSON(json: any): LoginResponse {
  return LoginResponseFromJSONTyped(json, false);
}

export function LoginResponseFromJSONTyped(
  json: any,
  ignoreDiscriminator: boolean
): LoginResponse {
  if (json === undefined || json === null) {
    return json;
  }
  return {
    loggedIn: !exists(json, "loggedIn") ? undefined : json["loggedIn"],
    user: !exists(json, "user") ? undefined : UserFromJSON(json["user"]),
  };
}

export function LoginResponseToJSON(value?: LoginResponse | null): any {
  if (value === undefined) {
    return undefined;
  }
  if (value === null) {
    return null;
  }
  return {
    loggedIn: value.loggedIn,
    user: UserToJSON(value.user),
  };
}
