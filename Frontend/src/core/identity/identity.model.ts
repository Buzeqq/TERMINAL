export interface Identity {
  isAuthenticated: boolean,
  email: string | null,
  role: 'administrator' | 'moderator' | 'user' | 'guest'
}

export interface LoginForm {
  email: string,
  password: string,
  rememberMe: boolean
}
