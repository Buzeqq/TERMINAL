export type Role = 'administrator' | 'moderator' | 'user' | 'guest';

export interface Identity {
  isAuthenticated: boolean;
  email: string | null;
  roles: Role[];
}

export interface LoginForm {
  email: string;
  password: string;
  rememberMe: boolean;
}
