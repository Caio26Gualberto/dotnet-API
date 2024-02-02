export interface IUserManagerResponse {
    message: string,
    token: string,
    isSuccess: boolean,
    firstTimeLogin: boolean,
    isSkipedFirstPage: boolean,
    errors: Array<String>
}