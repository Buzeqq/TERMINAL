export interface SelectedItem {
  type: 'Sample' | 'Project' | 'Recipe' | 'Tag'
  id: string
  config?: {
    init?: boolean
  }
}
