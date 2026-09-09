import Editor from '@monaco-editor/react'

import '@/lib/monaco'

type ComposeYamlEditorProps = {
  value: string
  onChange: (value: string) => void
  readOnly?: boolean
}

export function ComposeYamlEditor({
  value,
  onChange,
  readOnly = false,
}: ComposeYamlEditorProps) {
  return (
    <Editor
      height="55vh"
      defaultLanguage="yaml"
      theme="vs-dark"
      value={value}
      onChange={(next: string | undefined) => onChange(next ?? '')}
      options={{
        minimap: { enabled: false },
        fontSize: 13,
        wordWrap: 'on',
        scrollBeyondLastLine: false,
        readOnly,
        automaticLayout: true,
        tabSize: 2,
      }}
    />
  )
}
