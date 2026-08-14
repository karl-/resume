# {{Contact.Name}}

{{#if Contact.Address}}
**Address** {{Contact.Address}}
{{/if}}
{{#if Contact.Phone}}
**Phone** {{Contact.Phone}}
{{/if}}
{{#if Contact.Email}}
**Email** {{Contact.Email}}
{{/if}}
{{#if Contact.Website}}
**Website** {{markdown Contact.Website}}
{{/if}}

{{#if Summary}}
# Summary

{{{Summary}}}
{{/if}}

# Experience

{{#each Experience}}
## {{Employer}}

**Location** {{Location}}
**Title** {{Title}}
**Date** {{Date}}
{{#if Contact}}
**Contact**
{{{Contact}}}{{/if}}

{{{Description}}}

{{/each}}

# Skills & Other

{{#each Skills}}
- {{{this}}}
{{/each}}

# Education

{{#each Education}}
## {{School}}

**Location** {{Location}}
**Degree** {{Degree}}
**Date** {{Date}}

{{#if Awards}}
### Awards

{{#each Awards}}
- {{{this}}}
{{/each}}
{{/if}}

{{/each}}

# Presentations

{{#each Presentations}}
## {{Title}}

**Conference** {{Conference}}
**Location** {{Location}}
**Date** {{Date}}

{{/each}}
