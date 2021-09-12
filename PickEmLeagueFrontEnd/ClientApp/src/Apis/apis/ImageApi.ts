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

export interface ImageGetUsersImageGetRequest {
  userId?: number;
  access?: string;
  secret?: string;
}

export interface ImageSetUsersImagePostRequest {
  userId?: number;
  access?: string;
  secret?: string;
  formFile?: Blob;
}

/**
 *
 */
export class ImageApi extends runtime.BaseAPI {
  /**
   */
  async imageGetUsersImageGetRaw(
    requestParameters: ImageGetUsersImageGetRequest
  ): Promise<runtime.ApiResponse<string>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.userId !== undefined) {
      queryParameters["userId"] = requestParameters.userId;
    }

    if (requestParameters.access !== undefined) {
      queryParameters["access"] = requestParameters.access;
    }

    if (requestParameters.secret !== undefined) {
      queryParameters["secret"] = requestParameters.secret;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Image/getUsersImage`,
      method: "GET",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.TextApiResponse(response) as any;
  }

  /**
   */
  async imageGetUsersImageGet(
    requestParameters: ImageGetUsersImageGetRequest
  ): Promise<string> {
    const response = await this.imageGetUsersImageGetRaw(requestParameters);
    return await response.value();
  }

  /**
   */
  async imageSetUsersImagePostRaw(
    requestParameters: ImageSetUsersImagePostRequest
  ): Promise<runtime.ApiResponse<void>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.userId !== undefined) {
      queryParameters["userId"] = requestParameters.userId;
    }

    if (requestParameters.access !== undefined) {
      queryParameters["access"] = requestParameters.access;
    }

    if (requestParameters.secret !== undefined) {
      queryParameters["secret"] = requestParameters.secret;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const consumes: runtime.Consume[] = [
      { contentType: "multipart/form-data" },
    ];
    // @ts-ignore: canConsumeForm may be unused
    const canConsumeForm = runtime.canConsumeForm(consumes);

    let formParams: { append(param: string, value: any): any };
    let useForm = false;
    // use FormData to transmit files using content-type "multipart/form-data"
    useForm = canConsumeForm;
    if (useForm) {
      formParams = new FormData();
    } else {
      formParams = new URLSearchParams();
    }

    if (requestParameters.formFile !== undefined) {
      formParams.append("formFile", requestParameters.formFile as any);
    }

    const response = await this.request({
      path: `/Image/setUsersImage`,
      method: "POST",
      headers: headerParameters,
      query: queryParameters,
      body: formParams,
    });

    return new runtime.VoidApiResponse(response);
  }

  /**
   */
  async imageSetUsersImagePost(
    requestParameters: ImageSetUsersImagePostRequest
  ): Promise<void> {
    await this.imageSetUsersImagePostRaw(requestParameters);
  }
}
