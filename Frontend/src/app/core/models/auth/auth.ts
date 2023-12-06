export interface successfulLoginResponse {
  token: string
}

export interface decodedJWT {
  sub: string // token
  email: string
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string // role
  exp: number // expiration time - UTC timestamp in seconds
  iss: string // terminal
  aud: string // terminal-clients
}
