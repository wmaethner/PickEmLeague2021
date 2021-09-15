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
import { Game, GameFromJSON, GameToJSON } from "../models";

export interface GameAddGameSchedulePostRequest {
  csvFile?: Blob;
}

export interface GameCreateGameForWeekPostRequest {
  week?: number;
}

export interface GameDeleteDeleteRequest {
  id?: number;
}

export interface GameDeleteGameDeleteRequest {
  id?: number;
}

export interface GameGetGamesForWeekGetRequest {
  week?: number;
}

export interface GameGetGetRequest {
  id?: number;
}

export interface GameUpdatePutRequest {
  game?: Game;
}

/**
 *
 */
export class GameApi extends runtime.BaseAPI {
  /**
   */
  async gameAddGameSchedulePostRaw(
    requestParameters: GameAddGameSchedulePostRequest
  ): Promise<runtime.ApiResponse<void>> {
    const queryParameters: runtime.HTTPQuery = {};

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

    if (requestParameters.csvFile !== undefined) {
      formParams.append("csvFile", requestParameters.csvFile as any);
    }

    const response = await this.request({
      path: `/Game/add-game-schedule`,
      method: "POST",
      headers: headerParameters,
      query: queryParameters,
      body: formParams,
    });

    return new runtime.VoidApiResponse(response);
  }

  /**
   */
  async gameAddGameSchedulePost(
    requestParameters: GameAddGameSchedulePostRequest
  ): Promise<void> {
    await this.gameAddGameSchedulePostRaw(requestParameters);
  }

  /**
   */
  async gameCreateGameForWeekPostRaw(
    requestParameters: GameCreateGameForWeekPostRequest
  ): Promise<runtime.ApiResponse<Game>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.week !== undefined) {
      queryParameters["week"] = requestParameters.week;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/create-game-for-week`,
      method: "POST",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      GameFromJSON(jsonValue)
    );
  }

  /**
   */
  async gameCreateGameForWeekPost(
    requestParameters: GameCreateGameForWeekPostRequest
  ): Promise<Game> {
    const response = await this.gameCreateGameForWeekPostRaw(requestParameters);
    return await response.value();
  }

  /**
   */
  async gameCreatePostRaw(): Promise<runtime.ApiResponse<Game>> {
    const queryParameters: runtime.HTTPQuery = {};

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/create`,
      method: "POST",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      GameFromJSON(jsonValue)
    );
  }

  /**
   */
  async gameCreatePost(): Promise<Game> {
    const response = await this.gameCreatePostRaw();
    return await response.value();
  }

  /**
   */
  async gameCurrentWeekGetRaw(): Promise<runtime.ApiResponse<number>> {
    const queryParameters: runtime.HTTPQuery = {};

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/current-week`,
      method: "GET",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.TextApiResponse(response) as any;
  }

  /**
   */
  async gameCurrentWeekGet(): Promise<number> {
    const response = await this.gameCurrentWeekGetRaw();
    return await response.value();
  }

  /**
   */
  async gameDeleteDeleteRaw(
    requestParameters: GameDeleteDeleteRequest
  ): Promise<runtime.ApiResponse<void>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.id !== undefined) {
      queryParameters["id"] = requestParameters.id;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/delete`,
      method: "DELETE",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.VoidApiResponse(response);
  }

  /**
   */
  async gameDeleteDelete(
    requestParameters: GameDeleteDeleteRequest
  ): Promise<void> {
    await this.gameDeleteDeleteRaw(requestParameters);
  }

  /**
   */
  async gameDeleteGameDeleteRaw(
    requestParameters: GameDeleteGameDeleteRequest
  ): Promise<runtime.ApiResponse<void>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.id !== undefined) {
      queryParameters["id"] = requestParameters.id;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/delete-game`,
      method: "DELETE",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.VoidApiResponse(response);
  }

  /**
   */
  async gameDeleteGameDelete(
    requestParameters: GameDeleteGameDeleteRequest
  ): Promise<void> {
    await this.gameDeleteGameDeleteRaw(requestParameters);
  }

  /**
   */
  async gameGetAllGetRaw(): Promise<runtime.ApiResponse<Array<Game>>> {
    const queryParameters: runtime.HTTPQuery = {};

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/get-all`,
      method: "GET",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      jsonValue.map(GameFromJSON)
    );
  }

  /**
   */
  async gameGetAllGet(): Promise<Array<Game>> {
    const response = await this.gameGetAllGetRaw();
    return await response.value();
  }

  /**
   */
  async gameGetGamesForWeekGetRaw(
    requestParameters: GameGetGamesForWeekGetRequest
  ): Promise<runtime.ApiResponse<Array<Game>>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.week !== undefined) {
      queryParameters["week"] = requestParameters.week;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/get-games-for-week`,
      method: "GET",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      jsonValue.map(GameFromJSON)
    );
  }

  /**
   */
  async gameGetGamesForWeekGet(
    requestParameters: GameGetGamesForWeekGetRequest
  ): Promise<Array<Game>> {
    const response = await this.gameGetGamesForWeekGetRaw(requestParameters);
    return await response.value();
  }

  /**
   */
  async gameGetGetRaw(
    requestParameters: GameGetGetRequest
  ): Promise<runtime.ApiResponse<Game>> {
    const queryParameters: runtime.HTTPQuery = {};

    if (requestParameters.id !== undefined) {
      queryParameters["id"] = requestParameters.id;
    }

    const headerParameters: runtime.HTTPHeaders = {};

    const response = await this.request({
      path: `/Game/get`,
      method: "GET",
      headers: headerParameters,
      query: queryParameters,
    });

    return new runtime.JSONApiResponse(response, (jsonValue) =>
      GameFromJSON(jsonValue)
    );
  }

  /**
   */
  async gameGetGet(requestParameters: GameGetGetRequest): Promise<Game> {
    const response = await this.gameGetGetRaw(requestParameters);
    return await response.value();
  }

  /**
   */
  async gameUpdatePutRaw(
    requestParameters: GameUpdatePutRequest
  ): Promise<runtime.ApiResponse<void>> {
    const queryParameters: runtime.HTTPQuery = {};

    const headerParameters: runtime.HTTPHeaders = {};

    headerParameters["Content-Type"] = "application/json-patch+json";

    const response = await this.request({
      path: `/Game/update`,
      method: "PUT",
      headers: headerParameters,
      query: queryParameters,
      body: GameToJSON(requestParameters.game),
    });

    return new runtime.VoidApiResponse(response);
  }

  /**
   */
  async gameUpdatePut(requestParameters: GameUpdatePutRequest): Promise<void> {
    await this.gameUpdatePutRaw(requestParameters);
  }
}
