export interface IResult<T> {
  id: string;
  result: T;
  error: boolean;
  messageType: string;
  messages: string[];
}

export interface IResults<T> {
  totalRecords: number;
  results: T[];
  error: boolean;
  messageType: string;
  messages: string[];
}
