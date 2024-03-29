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

import * as runtime from "../runtime";
import {
  LoginResponse,
  LoginResponseFromJSON,
  LoginResponseToJSON,
} from "../models";

export interface AuthenticationAttemptLoginPostRequest {
  email?: string;
  passwordHash?: string;
}

/**
 *
 */
export class AuthenticationApi extends runtime.BaseAPI {
  /**
   */
  async authenticationAttemptLoginPostRaw(
    requestParameters: AuthenticationAttemptLoginPostRequest
  ): Promise<runtime.ApiResponse<LoginResponse>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.email !== undefined) {
      queryParameters["email"] = requestParameters.email;
    }

    if (requestParameters.passwordHash !== undefined) {
      queryParameters["passwordHash"] = requestParameters.passwordHash;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Authentication/attemptLogin`,
      method: "POST",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      LoginResponseFromJSON(jsonValue)
    );
  }

  /**
   */
  async authenticationAttemptLoginPost(
    requestParameters: AuthenticationAttemptLoginPostRequest
  ): Promise<LoginResponse> {
    const response = await this.authenticationAttemptLoginPostRaw(
      requestParameters
    );
    return await response.value();
  }
}
