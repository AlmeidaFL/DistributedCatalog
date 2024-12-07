import { Product } from "./product"

export interface CartResume {
    id: string
    products: Product[]
    total: number
}

