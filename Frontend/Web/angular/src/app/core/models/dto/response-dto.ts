export class ResponseDto {
    isSuccess: boolean = false;
    error: ErrorDto = new ErrorDto();
    data: any = null;
}

export class ErrorDto {
    errorCode: string = '';
}