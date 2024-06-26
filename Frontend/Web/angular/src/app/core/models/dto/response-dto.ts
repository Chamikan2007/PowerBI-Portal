export class ResponseDto {
    isSuccess: boolean = false;
    error: ErrorDto = new ErrorDto();
    data: any = null;
}

export interface ResponseModel<T>{
    isSuccess: boolean;
    error: ErrorDto;
    data: T;
}

export class ErrorDto {
    errorCode: string = '';
}