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
import { UserSummary, UserSummaryFromJSON, UserSummaryToJSON } from "../models";

export interface ScoreSummaryGetScoreSummariesGetRequest {
  week?: number;
}

/**
 *
 */
export class ScoreSummaryApi extends runtime.BaseAPI {
  /**
   */
  async scoreSummaryGetScoreSummariesGetRaw(
    requestParameters: ScoreSummaryGetScoreSummariesGetRequest
  ): Promise<runtime.ApiResponse<Array<UserSummary>>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.week !== undefined) {
      queryParameters["week"] = requestParameters.week;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/ScoreSummary/getScoreSummaries`,
      method: "GET",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      jsonValue.map(UserSummaryFromJSON)
    );
  }

  /**
   */
  async scoreSummaryGetScoreSummariesGet(
    requestParameters: ScoreSummaryGetScoreSummariesGetRequest
  ): Promise<Array<UserSummary>> {
    const response = await this.scoreSummaryGetScoreSummariesGetRaw(
      requestParameters
    );
    return await response.value();
  }
}
