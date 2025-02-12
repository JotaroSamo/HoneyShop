export enum ProductStatusEnum {
  InStock = 1, // В наличии
  OutOfStock = 2, // Нет в наличии
  NoData = 3 // Нет данных
}

export const ProductStatusEnumLabels = {
  [ProductStatusEnum.InStock]: 'В наличии',
  [ProductStatusEnum.OutOfStock]: 'Нет в наличии',
  [ProductStatusEnum.NoData]: 'Нет данных'
};
