export interface SelectedItem {
  type: 'Sample' | 'Project' | 'Recipe' | 'Tag' | 'User'
  id: string
  config?: {
    init?: boolean
  }
}
