export interface OrderParametersDto {
    sortColumn?: SortColumnOptions;
    sortOrder?: SortOrderOptions;
    pageNumber?: number;
    pageSize?: number;
}

export interface OrderPredictionParametersDto {
    sortColumn?: OrderPredictionSortColumnOptions;
    sortOrder?: OrderPredictionSortOrderOptions;
    pageNumber?: number;
    pageSize?: number;
    search?: string;
}

export interface CreateOrderParametersDto {
    order: OrderDto;
    orderDetailDtos: OrderDetailDto[];
}

export interface OrderDto {
    orderid?: number;
    custid: number;
    empid: number;
    orderdate: string;
    requireddate: string;
    shippeddate: string;
    shipperid: number;
    freight: number;
    shipname: string;
    shipaddress: string;
    shipcity: string;
    shipcountry: string;
}

export interface OrderDetailDto {
    product_id: number;
    unit_price: number;
    quantity: number;
    discount: number;
}

export interface ClientOrderDto {
    orderid: number;
    requireddate: string;
    shippeddate?: string;
    shipname: string;
    shipaddress: string;
    shipcity: string;
}

export interface PaginatedResult<T> {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
}

export interface CustomerOrderPredictionDto {
    customerId: string;
    companyName: string;
    lastOrderDate: string;
    nextPredictedOrder?: string;
}

export enum SortColumnOptions {
    OrderId = "orderid",
    RequiredDate = "requireddate",
    ShippedDate = "shippeddate",
    ShipName = "shipname",
    ShipAddress = "shipaddress",
    ShipCity = "shipcity"
}

export enum SortOrderOptions {
    Ascending = "ASC",
    Descending = "DESC"
}

export enum OrderPredictionSortColumnOptions {
    CompanyName = "CompanyName",
    LastOrderDate = "LastOrderDate",
    NextPredictedOrder = "NextPredictedOrder"
}

export enum OrderPredictionSortOrderOptions {
    Ascending = "ASC",
    Descending = "DESC"
}