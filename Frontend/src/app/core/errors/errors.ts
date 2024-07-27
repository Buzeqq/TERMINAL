export interface ProblemDetails {
  title: string;
  type: string;
  status: number;
  detail?: string;
  instance?: string;
  errors: { [key: string]: string[] }[];
}

export class TerminalError extends Error implements ProblemDetails {
  title: string;
  detail?: string;
  type: string;
  status: number;
  instance?: string;
  errors: { [key: string]: string[]; }[];

  constructor(error: ProblemDetails) {
    super();
    this.title = error.title;
    this.detail = error.detail;
    this.status = error.status;
    this.type = error.type;
    this.errors = error.errors;
  }
}

export class NotAuthorizedError extends TerminalError {}
export class FailedToLoginError extends TerminalError {
  constructor(error: ProblemDetails) {
    super(error);
  }
}
