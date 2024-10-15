export interface Entity {
  id: string;
}

export interface PaginatedResponse<T> {
  data: readonly T[];
  totalCount: number;
  pageIndex: number;
  pageSize: number;
}
