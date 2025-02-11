export interface PaginationListModel<T> {
    page: number;       // Номер текущей страницы
    size: number;       // Размер страницы (количество элементов на странице)
    total: number;      // Общее количество элементов
    models: T[];        // Массив элементов типа T
  }